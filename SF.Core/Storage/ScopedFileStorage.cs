/*******************************************************************************
* 命名空间: SF.Core.Storage
*
* 功 能： N/A
* 类 名： ScopedFileStorage
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2017/1/17 15:05:05 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Storage
{
    public class ScopedFileStorage : IFileStorage
    {
        private readonly string _pathPrefix;

        public ScopedFileStorage(IFileStorage storage, string scope)
        {
            UnscopedStorage = storage;
            Scope = !String.IsNullOrWhiteSpace(scope) ? scope.Trim() : null;
            _pathPrefix = Scope != null ? String.Concat(Scope, "/") : String.Empty;
        }

        public IFileStorage UnscopedStorage { get; private set; }

        public string Scope { get; private set; }

        public Task<Stream> GetFileStreamAsync(string path, CancellationToken cancellationToken = new CancellationToken())
        {
            return UnscopedStorage.GetFileStreamAsync(String.Concat(_pathPrefix, path), cancellationToken);
        }

        public async Task<FileSpec> GetFileInfoAsync(string path)
        {
            var file = await UnscopedStorage.GetFileInfoAsync(String.Concat(_pathPrefix, path)).AnyContext();
            if (file != null)
                file.Path = file.Path.Substring(_pathPrefix.Length);

            return file;
        }

        public Task<bool> ExistsAsync(string path)
        {
            return UnscopedStorage.ExistsAsync(String.Concat(_pathPrefix, path));
        }

        public Task<bool> SaveFileAsync(string path, Stream stream, CancellationToken cancellationToken = new CancellationToken())
        {
            return UnscopedStorage.SaveFileAsync(String.Concat(_pathPrefix, path), stream, cancellationToken);
        }

        public Task<bool> RenameFileAsync(string path, string newpath, CancellationToken cancellationToken = new CancellationToken())
        {
            return UnscopedStorage.RenameFileAsync(String.Concat(_pathPrefix, path), String.Concat(_pathPrefix, newpath), cancellationToken);
        }

        public Task<bool> CopyFileAsync(string path, string targetpath, CancellationToken cancellationToken = new CancellationToken())
        {
            return UnscopedStorage.CopyFileAsync(String.Concat(_pathPrefix, path), String.Concat(_pathPrefix, targetpath), cancellationToken);
        }

        public Task<bool> DeleteFileAsync(string path, CancellationToken cancellationToken = new CancellationToken())
        {
            return UnscopedStorage.DeleteFileAsync(String.Concat(_pathPrefix, path), cancellationToken);
        }

        public async Task<IEnumerable<FileSpec>> GetFileListAsync(string searchPattern = null, int? limit = null, int? skip = null, CancellationToken cancellationToken = new CancellationToken())
        {
            if (String.IsNullOrEmpty(searchPattern))
                searchPattern = "*";

            var files = (await UnscopedStorage.GetFileListAsync(String.Concat(_pathPrefix, searchPattern), limit, skip, cancellationToken).AnyContext()).ToList();
            foreach (var file in files)
                file.Path = file.Path.Substring(_pathPrefix.Length);

            return files;
        }

        public void Dispose() { }
    }
}

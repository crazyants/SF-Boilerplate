/// <binding AfterBuild='copy-modules' />
//"use strict";

var gulp = require('gulp')

//var paths = {
//    devModules: "../Modules/",
//    hostModules: "./Modules/"
//};

//var modules = [
//    'SimpleFramework.Module.Web'
//];

//gulp.task('clean-module', function () {
//    return gulp.src([paths.hostModules + '*'], { read: false })
//    .pipe(clean());
//});


//gulp.task('copy-modules', ['clean-module'], function () {
//    modules.forEach(function (module) {
//        gulp.src([paths.devModules + module + '/Views/**/*.*', paths.devModules + module + '/wwwroot/**/*.*'], { base: module })
//            .pipe(gulp.dest(paths.hostModules + module));
//        gulp.src(paths.devModules + module + '/bin/Debug/netstandard1.6/**/*.*')
//            .pipe(gulp.dest(paths.hostModules + module + '/bin'));
//    });
//});
//var gulp = require("gulp");

//gulp.task(
//  "copy-extensions", function (cb) {
//      gulp.src(["../WebApplication.ExtensionA/bin/Debug/netstandard1.6/WebApplication.ExtensionA.dll"]).pipe(gulp.dest("Extensions"));
//      gulp.src(["../WebApplication.ExtensionB/bin/Debug/netstandard1.6/WebApplication.ExtensionB.dll"]).pipe(gulp.dest("Extensions"));
//      gulp.src(["../WebApplication.ExtensionB.Data.Abstractions/bin/Debug/netstandard1.6/WebApplication.ExtensionB.Data.Abstractions.dll"]).pipe(gulp.dest("Extensions"));
//      gulp.src(["../WebApplication.ExtensionB.Data.EntityFramework.Sqlite/bin/Debug/netstandard1.6/WebApplication.ExtensionB.Data.EntityFramework.Sqlite.dll"]).pipe(gulp.dest("Extensions"));
//      gulp.src(["../WebApplication.ExtensionB.Data.Models/bin/Debug/netstandard1.6/WebApplication.ExtensionB.Data.Models.dll"]).pipe(gulp.dest("Extensions"));
//      cb();
//  }
//);
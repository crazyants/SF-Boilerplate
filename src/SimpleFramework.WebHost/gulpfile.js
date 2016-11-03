/// <binding AfterBuild='copy-modules' />
"use strict";

var gulp = require('gulp'),
    clean = require('gulp-clean');

var paths = {
    devModules: "../Modules/",
    hostModules: "./Modules/"
};

var modules = [
    'SimpleFramework.Module.ActivityLog',
    'SimpleFramework.Module.Backend',
    'SimpleFramework.Module.Localization'
];

gulp.task('clean-module', function () {
    return gulp.src([paths.hostModules + '*'], { read: false })
    .pipe(clean());
});


gulp.task('copy-modules', ['clean-module'], function () {
    modules.forEach(function (module) {
        gulp.src([paths.devModules + module + '/Views/**/*.*', paths.devModules + module + '/wwwroot/**/*.*'], { base: module })
            .pipe(gulp.dest(paths.hostModules + module));
        gulp.src(paths.devModules + module + '/bin/Debug/netstandard1.6/**/' + module + '.*')
            .pipe(gulp.dest(paths.hostModules + module + '/bin'));
    });

    //gulp.src(paths.devModules + 'SimpleFramework.Module.SampleData/SampleContent/**/*.*')
    //        .pipe(gulp.dest(paths.hostModules + 'SimpleFramework.Module.SampleData/SampleContent'));
});
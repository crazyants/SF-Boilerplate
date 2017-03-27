﻿/// <binding AfterBuild='copy-modules' />
"use strict";

var gulp = require('gulp'),
    clean = require('gulp-clean');

var paths = {
    devModules: "../Modules/",
    hostModules: "./Modules/"
};

var modules = [
    'SF.Module.ActivityLog',
    'SF.Module.LoggingStorage',
    'SF.Module.Backend',
    'SF.Module.Localization',
    'SF.Module.Demo',
    'SF.Module.Blog'
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
        gulp.src(paths.devModules + module + '/bin/Debug/netcoreapp1.0/**/' + module + '.*')
            .pipe(gulp.dest(paths.hostModules + module + '/bin'));
        gulp.src(paths.devModules + module + '/bin/Debug/netcoreapp1.1/**/' + module + '.*')
            .pipe(gulp.dest(paths.hostModules + module + '/bin'));
        gulp.src(paths.devModules + module + '/module.json')
            .pipe(gulp.dest(paths.hostModules + module));
    });

});
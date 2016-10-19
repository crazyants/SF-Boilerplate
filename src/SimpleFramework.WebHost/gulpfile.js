/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require("gulp"),
  autoprefixer = require("gulp-autoprefixer"),
  concat = require("gulp-concat"),
  del = require("del"),
  minifyCss = require("gulp-minify-css"),
  rename = require("gulp-rename"),
  runSequence = require("run-sequence"),
  sass = require("gulp-sass"),
  tsc = require("gulp-tsc"),
  uglify = require("gulp-uglify");

gulp.task('default', function () {
    // place code for your default task here
});
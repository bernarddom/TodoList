<?php

use App\Http\Controllers\Todo\TodoListController;
use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return view('welcome');
});
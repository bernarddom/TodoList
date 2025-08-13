<?php

use App\Http\Controllers\Todo\CategoryController;
use App\Http\Controllers\Todo\TodoList;
use App\Http\Controllers\Todo\TodoListController;
use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return view('welcome');
}); 

Route::get('/flights', function () {

    Route::get('/user/{id}', [TodoListController::class, 'show']);

})->middleware('auth');

Route::prefix('todo')->group(function () {
    Route::get('', [TodoListController::class, 'show']);
    Route::get('category', [CategoryController::class, 'getCategories']);
    Route::post('category', [CategoryController::class, 'store']);
});
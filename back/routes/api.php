<?php

use App\Http\Controllers\Todo\TodoList;
use App\Http\Controllers\Todo\TodoListController;
use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return view('welcome');
}); 

Route::get('/flights', function () {

    Route::get('/user/{id}', [TodoListController::class, 'show']);

})->middleware('auth');

Route::prefix('list')->group(function () {
    Route::get('', [TodoListController::class, 'show']);
});
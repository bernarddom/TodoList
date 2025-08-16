<?php

use App\Http\Controllers\Todo\CategoryController;
use App\Http\Controllers\Todo\TodoListController;
use Illuminate\Http\Client\Request;
use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return view('welcome');
}); 

Route::get('/flights', function () {

    Route::get('/user/{id}', [TodoListController::class, 'show']);

})->middleware('auth');

Route::prefix('todo')->group(function () {
    //Lists
    Route::get('list', [TodoListController::class, 'show']);

    Route::get('category', [CategoryController::class, 'getCategories']);
    Route::post('category', [CategoryController::class, 'store']);
});

Route::get('/token', function (Request $request) {
    return json_encode(["token" => csrf_token()]);
});
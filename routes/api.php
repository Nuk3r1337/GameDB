<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Models\Game;
use App\Models\Barcode;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/
Route::resource('games', 'App\Http\Controllers\GameController');
Route::resource('comments', 'App\Http\Controllers\CommentController');
Route::resource('barcode', 'App\Http\Controllers\BarcodeController');
Route::resource('users', 'App\Http\Controllers\UserController');
Route::resource('agerating','App\Http\Controllers\Age_ratingController');
Route::resource('publisher','App\Http\Controllers\PublisherController');
Route::resource('genres','App\Http\Controllers\GenreController');

Route::get('agerating/{id}/games', 'App\Http\Controllers\Age_ratingController@show');
Route::get('publisher/{id}/games','App\Http\Controllers\publisherController@show');
Route::get('user/{id}/games', 'App\Http\Controllers\UserHasGamesController@index');
Route::get('genre/{id}/games','App\Http\Controllers\GenreController@show');



Route::get('/test', function (){
    return ['message' => 'hello'];
});

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});

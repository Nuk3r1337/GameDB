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
Route::resource('barcodes', 'App\Http\Controllers\BarcodeController');
Route::resource('users', 'App\Http\Controllers\UserController');
Route::resource('ageratings','App\Http\Controllers\Age_ratingController');
Route::resource('publishers','App\Http\Controllers\PublisherController');
Route::resource('genres','App\Http\Controllers\GenreController');
Route::resource('gamegenres', 'App\Http\Controllers\GamesHasGenresController');
Route::resource('usergames', 'App\Http\Controllers\UserHasGamesController');

Route::get('agerating/{id}/games', 'App\Http\Controllers\Age_ratingController@show');
Route::get('publisher/{id}/games','App\Http\Controllers\publisherController@show');
Route::get('user/{id}/games', 'App\Http\Controllers\UserHasGamesController@index');
Route::get('genre/{id}/games','App\Http\Controllers\GamesHasGenresController@show');



Route::get('/test', function (){
    return ['message' => 'hello'];
});

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});

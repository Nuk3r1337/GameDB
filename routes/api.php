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
Route::get('user/{id}/games', 'App\Http\Controllers\UserHasGamesController@index');

Route::get('/test', function (){
    return ['message' => 'hello'];
});

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});
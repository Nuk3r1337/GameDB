<?php

use Illuminate\Support\Facades\Route;

use Illuminate\Http\Request;
use Spatie\Permission\Models\Role;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {

    // create role to api guard
    // Role::create(['guard_name' => 'api', 'name' => 'admin']);

    // assign role to user
    // auth()->user()->assignRole('admin');

    // string array assigned roles to the user
    // auth()->user()->getRoleNames();

    //return auth()->user()->getRoleNames();

    return view('welcome');
});

Route::get('/test', function (){
    return ['message' => 'hello'];
});

Route::get('/home', [App\Http\Controllers\HomeController::class, 'index'])->name('home');

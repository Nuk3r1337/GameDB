<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class User_rating extends Model
{
    protected $fillable = [
        'rating',
        'games_rating',
        'users_id'
    ];
}

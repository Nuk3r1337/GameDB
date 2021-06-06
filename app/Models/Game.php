<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Game extends Model
{
    protected $fillable = [
        'title',
        'release_date',
        'description',
        'publishers_id',
        'rating_id',
        'picture'
    ];

    protected $casts = [
        'release_date' => 'datetime',
        'publishers_id' => 'integer',
        'rating_id' => 'integer'
    ];
}

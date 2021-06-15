<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Games_has_genre extends Model
{
    protected $fillable = [
        'games_id',
        'genre_id'
    ];

    protected $casts = [
        'games_id' => 'integer',
        'genre_id' => 'integer'
    ];

    public function game(){
        return $this->belongsTo(Game::class, 'id','games_id');
    }
    public function genre(){
        return $this->hasOne(Genre::class, 'id', 'genre_id');
    }

}

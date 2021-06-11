<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class User_has_game extends Model
{
    protected $fillable = [
        'games_id',
        'user_id'
    ];

    protected $casts = [
        'games_id' => 'integer',
        'user_id' => 'integer'
    ];

    public function game(){
        return $this->belongsTo(Game::class, 'id','games_id');
    }
    public function user(){
        return $this->belongsTo(Genre::class, 'id', 'user_id');
    }
}

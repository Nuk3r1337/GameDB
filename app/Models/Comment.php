<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Comment extends Model
{
    protected $fillable = [
        'comments',
        'games_id',
        'users_id'
    ];

    public function game(){
        return $this->belongsTo(Game::class,'id','games_id');
    }
}

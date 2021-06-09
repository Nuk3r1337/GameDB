<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Genre extends Model
{
    protected $fillable = [
        'name'
    ];

    public function genreGame(){
        return $this->hasMany(Games_has_genre::class, 'genre_id', 'id');
    }
}

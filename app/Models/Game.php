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

    public function publisher(){
        return $this->belongsTo(Publisher::class, 'publishers_id','id');
    }

    public function ageRating(){
        return $this->belongsTo(Age_rating::class, 'age_ratings_id','id');
    }

    public function comments(){
        return $this->hasMany(Comment::class, 'games_id','id')->with('user');
    }

    public function gameGenre(){
        return $this->hasMany(Games_has_genre::class, 'games_id','id');
    }
}

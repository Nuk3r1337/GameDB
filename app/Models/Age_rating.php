<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Age_rating extends Model
{
    protected $fillable = [
        'age_rating',
    ];

    public function game(){
        return $this->HasMany(Game::class);
    }
}

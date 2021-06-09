<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class AgeRatingSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('age_ratings')->insert([
            ['age_rating' => 'unknown',
                'created_at' => now(),
                'updated_at' => now()],
            ['age_rating' => 'PEGI 3' ,
                'created_at' => now(),
                'updated_at' => now()],
            ['age_rating' => 'PEGI 7' ,
                'created_at' => now(),
                'updated_at' => now()],
            ['age_rating' => 'PEGI 12' ,
                'created_at' => now(),
                'updated_at' => now()],
            ['age_rating' => 'PEGI 16' ,
                'created_at' => now(),
                'updated_at' => now()],
            ['age_rating' => 'PEGI 18' ,
                'created_at' => now(),
                'updated_at' => now()]
        ]);
    }
}

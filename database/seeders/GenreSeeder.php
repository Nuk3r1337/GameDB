<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;


class GenreSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('genres')->insert([
            ['name' => 'unknown',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'MMO',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'MOBA',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'FPS',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'RPG',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Survival',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Adventure',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Sandbox',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Puzzle',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Platformer',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Party',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Simulation',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Strategy',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Tower Defence',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Visual Novel',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Fighting',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Stealth',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Rhythm',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Battle Royal',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Horror',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Sport',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Racing',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Gacha',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Open World',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Rouge-Like',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Metroidvania',
                'created_at' => now(),
                'updated_at' => now()],
        ]);
    }
}

<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class PublisherSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('publishers')->insert([
            ['name' => 'unknown',
            'created_at' => now(),
            'updated_at' => now()],
            ['name' => 'Nintendo',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Sega',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Sony',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Microsoft',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Bathesda',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Activision Blizzard',
                'created_at' => now(),
                'updated_at' => now()],
                ['name' => 'Square Enix',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Bandai Namco',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Capcom',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'EA',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Valve',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Konami',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Epic Games',
                'created_at' => now(),
                'updated_at' => now()],
            ['name' => 'Ubisoft',
                'created_at' => now(),
                'updated_at' => now()]
        ]);
    }
}

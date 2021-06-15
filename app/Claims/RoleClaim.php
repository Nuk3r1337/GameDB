<?php

namespace App\Claims;

use CorBosman\Passport\AccessToken;
use App\User;

class RoleClaim
{
    public function handle(AccessToken $token, $next)
    {
        //$user = User::find($token->getUserIdentifier());

        //$roles = $user->getRoleNames();

        // auth()->user()->getRoleNames();

        //foreach ($roles as $role) {
        //    $token->addClaim('role', $role);
        //}

        $token->addClaim('role', 'user');

        return $next($token);
    }
}

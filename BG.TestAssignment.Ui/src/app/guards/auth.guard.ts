import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state, ) => {
  const as = inject(AuthService);
  const router = inject(Router);
  if(!as.isAutenticated())
    router.navigate(['']);
  return true;
};

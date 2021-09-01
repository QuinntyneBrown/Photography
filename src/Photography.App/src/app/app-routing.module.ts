import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core';
import { Route } from '@shared';

const routes: Routes = [

  Route.withShell([
    { path: '', redirectTo: 'landing', pathMatch: 'full' },
    { path: 'photos', loadChildren: () => import('./photos/photos.module').then(m => m.PhotosModule) },
    { path: 'landing', loadChildren: () => import('./landing/landing.module').then(m => m.LandingModule) },

    { path: 'about', loadChildren: () => import('./about/about.module').then(m => m.AboutModule) },
  ]),
  { path: 'workspace', loadChildren: () => import('./workspace/workspace.module').then(m => m.WorkspaceModule), canActivate: [AuthGuard] },
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

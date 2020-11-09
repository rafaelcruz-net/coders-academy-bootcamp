import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';

const routes: Routes = [
  {
    path: 'music',
    loadChildren: () =>
      import('./music-grid/music-grid.module').then((m) => m.MusicGridModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

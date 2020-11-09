import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MusicGridRoutingModule } from './music-grid-routing.module';
import { MusicListComponent } from './page/music-list/music-list.component';
import { MusicGridListComponent } from './components/music-grid-list/music-grid-list.component';
import { MaterialModule } from '../material.module';

@NgModule({
  declarations: [MusicListComponent, MusicGridListComponent],
  imports: [CommonModule, MaterialModule, MusicGridRoutingModule],
})
export class MusicGridModule {}

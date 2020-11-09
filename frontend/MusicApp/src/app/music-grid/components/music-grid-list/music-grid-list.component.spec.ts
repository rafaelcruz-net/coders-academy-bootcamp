import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicGridListComponent } from './music-grid-list.component';

describe('MusicGridListComponent', () => {
  let component: MusicGridListComponent;
  let fixture: ComponentFixture<MusicGridListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MusicGridListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicGridListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

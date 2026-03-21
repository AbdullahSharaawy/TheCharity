import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrgUserChioce } from './org-user-choice';

describe('OrgUserChioce', () => {
  let component: OrgUserChioce;
  let fixture: ComponentFixture<OrgUserChioce>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrgUserChioce],
    }).compileComponents();

    fixture = TestBed.createComponent(OrgUserChioce);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

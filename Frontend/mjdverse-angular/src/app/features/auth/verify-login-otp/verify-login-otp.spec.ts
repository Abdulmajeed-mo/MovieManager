import { ComponentFixture, TestBed } from '@angular/core/testing';
import { VerifyLoginOtp } from './verify-login-otp';

describe('VerifyLoginOtp', () => {
  let component: VerifyLoginOtp;
  let fixture: ComponentFixture<VerifyLoginOtp>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VerifyLoginOtp],
    }).compileComponents();

    fixture = TestBed.createComponent(VerifyLoginOtp);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

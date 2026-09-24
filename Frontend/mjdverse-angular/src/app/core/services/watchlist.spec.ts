import { TestBed } from '@angular/core/testing';
import { Watchlist } from './watchlist';

describe('Watchlist', () => {
  let service: Watchlist;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Watchlist);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

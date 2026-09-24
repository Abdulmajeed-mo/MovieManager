import { TestBed } from '@angular/core/testing';
import { WatchHistory } from './watch-history';

describe('WatchHistory', () => {
  let service: WatchHistory;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WatchHistory);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

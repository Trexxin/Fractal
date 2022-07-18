import { Component, OnInit } from '@angular/core';
import { Member } from '../models/member';
import { MembersService } from '../services/members.service';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrls: ['./browse.component.css']
})
export class BrowseComponent implements OnInit {
  members: Partial<Member[]>;
  predicate = 'liked';

  constructor(private memberService: MembersService) {
    
   }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate).subscribe(response => {
      this.members = response;
    })
  }

}

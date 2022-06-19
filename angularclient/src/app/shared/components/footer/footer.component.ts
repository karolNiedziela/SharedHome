import { faGithub } from '@fortawesome/free-brands-svg-icons';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  githubIcon = faGithub;
  githubPath: string = 'https://github.com/karolNiedziela/SharedHome';
  constructor() {}

  ngOnInit(): void {}
}

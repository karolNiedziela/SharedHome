import { Component, OnInit } from '@angular/core';
import { faGithub } from '@fortawesome/free-brands-svg-icons/faGithub';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  githubPath: string = 'https://github.com/karolNiedziela/SharedHome';
  githubIcon = faGithub;
  constructor() {}

  ngOnInit(): void {}
}

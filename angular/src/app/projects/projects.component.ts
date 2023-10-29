import { Component } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  //selector: 'app-projects',
  templateUrl: './projects.component.html',
  animations: [appModuleAnimation()]
  //styleUrls: ['./projects.component.css']
})
export class ProjectsComponent {

}

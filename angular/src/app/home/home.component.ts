import { result } from 'lodash-es';
import { ProjectService } from '@app/service/api/project.service';
import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent extends AppComponentBase implements OnInit {

  public total: TotalDto;
  constructor(injector: Injector,
    public projectService: ProjectService) {
    super(injector);
  }

  ngOnInit(): void {
    this.getTotal();
  }

  getTotal() {
    this.projectService.getAllTotal().subscribe((res) => {
      this.total = res.result;
    })
  }
}

export class TotalDto {
  countProject: number;
  countUser: number;
  countTicket: number;
  countTask: number;
}

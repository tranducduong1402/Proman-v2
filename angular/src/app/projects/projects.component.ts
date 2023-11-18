import { EditProjectComponent } from './edit-project/edit-project.component';
import { CreateProjectComponent } from './create-project/create-project.component';
import { ProjectService } from './../service/api/project.service';
import { Component, Injector } from '@angular/core';
import { UserService } from '@app/service/api/user.service';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  //selector: 'app-projects',
  templateUrl: './projects.component.html',
  animations: [appModuleAnimation()]
  //styleUrls: ['./projects.component.css']
})
export class ProjectsComponent extends PagedListingComponentBase<ProjectDto> {
  projects: ProjectDto[] = [];
  keyword: string = "";
  isActive: boolean | null;
  advancedFiltersVisible = false;

  public isLoading: boolean = false;
  public inputRequest = {} as PagedRequestDto;
  public status: boolean = null;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private userService: UserService,
    private projectService: ProjectService
  ) {
    super(injector);
  }

  createProject(): void {
    this.showCreateOrEditProjectDialog();
  }

  editProject(user: ProjectDto): void {
    this.showCreateOrEditProjectDialog(user.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.filterItems = [];
    this.inputRequest = request
    ;
    if (this.keyword) {
      request.searchText = this.keyword;
    }

    if (this.isActive) {
      request.isActive = this.isActive;
      this.status = this.isActive;
    }

    this.isLoading = true;
    this.projectService
      .getAllPaging(this.inputRequest)
      .pipe(finalize(() => {
        finishedCallback();
      }))
      .subscribe((result: any) => {
        this.projects = result.result.items;

        this.showPaging(result.result, pageNumber);
        this.isLoading = false;
      },()=> this.isLoading = false);
  }

  protected delete(project: ProjectDto): void {
    abp.message.confirm(
      this.l('Project Delete Warning Message', project.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this.projectService.delete(project.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditProjectDialog(id?: number): void {
    let createOrEditProjectDialog: BsModalRef;
    if (!id) {
      createOrEditProjectDialog = this._modalService.show(
        CreateProjectComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditProjectDialog = this._modalService.show(
        EditProjectComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditProjectDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}

export class ProjectDto {
  id: number;
  name: string;
  note: string;
  countMember: number;
  status: string;
  clientId: number;
  clientEmailAddress: string;
}

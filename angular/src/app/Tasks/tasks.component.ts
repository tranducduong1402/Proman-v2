import { TaskService } from './../service/api/task.service';
import { TicketService } from './../service/api/ticket.service';
import { ColumnStatusService } from './../service/api/column-status.service';
import { Component, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';
import { EditTaskComponent } from './edit-task/edit-task.component';
import { CreateTaskComponent } from './create-task/create-task.component';


class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  // selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  animations: [appModuleAnimation()]
  //styleUrls: ['./tasks.component.css']
})
export class TaksComponent extends PagedListingComponentBase<TaskDto> {
  tasks: TaskDto[] = [];
  keyword: string = "";
  isActive: boolean | null;
  advancedFiltersVisible = false;

  public isLoading: boolean = false;
  public inputRequest = {} as PagedRequestDto;
  public status: boolean = null;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    // private userService: UserService,
    // private columnStatusService: ColumnStatusService,
    private taskService:  TaskService,
  ) {
    super(injector);
  }

  createTask(): void {
    this.showCreateOrEditTaskDialog();
  }

  editTask(task: TaskDto): void {
    this.showCreateOrEditTaskDialog(task.id);
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
    this.taskService
      .getAllPaging(this.inputRequest)
      .pipe(finalize(() => {
        finishedCallback();
      }))
      .subscribe((result: any) => {
        this.tasks = result.result.items;

        this.showPaging(result.result, pageNumber);
        this.isLoading = false;
      },()=> this.isLoading = false);
  }

  protected delete(task: TaskDto): void {
    abp.message.confirm(
      this.l('Task Delete Warning Message', task.title),
      undefined,
      (result: boolean) => {
        if (result) {
          this.taskService.delete(task.id).subscribe(() => {
            abp.notify.success(this.l('Successfully Deleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditTaskDialog(id?: number): void {
    let showCreateOrEditTaskDialog: BsModalRef;
    if (!id) {
      showCreateOrEditTaskDialog = this._modalService.show(
        CreateTaskComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      showCreateOrEditTaskDialog = this._modalService.show(
        EditTaskComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    showCreateOrEditTaskDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}

export class TaskDto {
  id: number;
  title: string;
  description: string;
  // type: number;
  // priority: number;
  columnStatusName: string; 
  userName: string;
  ticketTitle: string
}

import { ColumnStatusService } from './../service/api/column-status.service';
import { Component, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';
import { CreateColumnStatusComponent } from './create-column-status/create-column-status.component';
import { EditColumnStatusComponent } from './edit-column-status/edit-column-status.component';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  // selector: 'app-column-status',
  templateUrl: './column-statuses.component.html',
  animations: [appModuleAnimation()]
  //styleUrls: ['./column-statuses.component.css']
})
export class ColumnStatusesComponent extends PagedListingComponentBase<ColumnStatusDto> {
  columnStatuses: ColumnStatusDto[] = [];
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
    private columnStatusService: ColumnStatusService
  ) {
    super(injector);
  }

  createColumnStatus(): void {
    this.showCreateOrEditColumnStatusDialog();
  }

  editColumnStatus(user: ColumnStatusDto): void {
    this.showCreateOrEditColumnStatusDialog(user.id);
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
    this.columnStatusService
      .getAllPaging(this.inputRequest)
      .pipe(finalize(() => {
        finishedCallback();
      }))
      .subscribe((result: any) => {
        this.columnStatuses = result.result.items;

        this.showPaging(result.result, pageNumber);
        this.isLoading = false;
      },()=> this.isLoading = false);
  }

  protected delete(columnStatus: ColumnStatusDto): void {
    abp.message.confirm(
      this.l('ColumnStatus Delete Warning Message', columnStatus.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this.columnStatusService.delete(columnStatus.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditColumnStatusDialog(id?: number): void {
    let showCreateOrEditColumnStatusDialog: BsModalRef;
    if (!id) {
      showCreateOrEditColumnStatusDialog = this._modalService.show(
        CreateColumnStatusComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      showCreateOrEditColumnStatusDialog = this._modalService.show(
        EditColumnStatusComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    showCreateOrEditColumnStatusDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}

export class ColumnStatusDto {
  id: number;
  name: string;
  type: number;
}

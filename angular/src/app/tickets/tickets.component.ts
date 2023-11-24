import { TicketService } from './../service/api/ticket.service';
import { ColumnStatusService } from './../service/api/column-status.service';
import { Component, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';
import { EditTicketComponent } from './edit-ticket/edit-ticket.component';
import { CreateTicketComponent } from './create-ticket/create-ticket.component';
import { CommentComponent } from '@app/comment/comment.component';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  // selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  animations: [appModuleAnimation()]
  //styleUrls: ['./tickets.component.css']
})
export class TicketsComponent extends PagedListingComponentBase<TicketDto> {
  tickets: TicketDto[] = [];
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
    private ticketService:  TicketService,
  ) {
    super(injector);
  }

  createTicket(): void {
    this.showCreateOrEditTicketDialog();
  }

  editTicket(ticket: TicketDto): void {
    this.showCreateOrEditTicketDialog(ticket.id);
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
    this.ticketService
      .getAllPaging(this.inputRequest)
      .pipe(finalize(() => {
        finishedCallback();
      }))
      .subscribe((result: any) => {
        this.tickets = result.result.items;

        this.showPaging(result.result, pageNumber);
        this.isLoading = false;
      },()=> this.isLoading = false);
  }

  protected delete(ticket: TicketDto): void {
    abp.message.confirm(
      this.l('Tickets Delete Warning Message', ticket.title),
      undefined,
      (result: boolean) => {
        if (result) {
          this.ticketService.delete(ticket.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditTicketDialog(id?: number): void {
    let showCreateOrEditTicketDialog: BsModalRef;
    if (!id) {
      showCreateOrEditTicketDialog = this._modalService.show(
        CreateTicketComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      showCreateOrEditTicketDialog = this._modalService.show(
        EditTicketComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    showCreateOrEditTicketDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  openComment(e): void {
    this.showDialogComment(e);
  }

  showDialogComment(ticket: any): void {
    let createOrEditProjectDialog: BsModalRef;
    if(ticket) {
      createOrEditProjectDialog = this._modalService.show(
        CommentComponent,
        {
          class: 'modal-lg',
          initialState: {
            ticketID: ticket.id,
          }
        }
      );
    }
  }
}

export class TicketDto {
  id: number;
  title: string;
  description: string;
  columnStatusName: string; 
  projectName: string;
}

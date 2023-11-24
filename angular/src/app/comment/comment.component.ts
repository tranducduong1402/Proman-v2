import { DatePipe } from "@angular/common";
import { Component, Injector, OnInit } from "@angular/core";
import { CommentService } from "@app/service/api/comment-service";
import { AppComponentBase } from "@shared/app-component-base";
import { BsModalRef } from "ngx-bootstrap/modal";
declare var Quill: any;
@Component({
  selector: "app-comment",
  templateUrl: "./comment.component.html",
  styleUrls: ["./comment.component.css"],
  providers: [DatePipe],
})
export class CommentComponent extends AppComponentBase implements OnInit {
  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private datePipe: DatePipe,
    private commentSV: CommentService
  ) {
    super(injector);
  }
  userInfo: any;
  activeIndex = -1;
  activeEdit = -1;
  editor = null;
  commentEditText = "";

  ngOnInit(): void {
    this.userInfo = this.appSession.user;
    this.getData();
  }

  comments = [];

  ticketID: any = null;

  newCommentText: string = "";

  edittingText: string = "";

  ngAfterViewInit(): void {
    this.initQuillEditor();
  }

  getData(): void {
    this.commentSV.getAll(this.ticketID).subscribe((res) => {
      if (res.result) {
        this.comments = res.result;
      }
    });
  }

  initQuillEditor(): void {
    this.editor = new Quill("#editor-container", {
      theme: "snow",
      placeholder: "Type your comment here...",
    });

    this.editor.on("text-change", () => {
      this.newCommentText = this.editor.root.innerHTML;
    });
  }

  addComment() {
    if (this.newCommentText.trim() !== "") {
      let newComment = {
        Description: this.newCommentText.trim(),
        TicketID: 5,
        UserId: this.userInfo.id,
      };
      this.commentSV.create(newComment).subscribe((res) => {
        this.comments.unshift(res.result);
        this.newCommentText = "";
      });
    }
  }

  onMouseEnter(cmt, index: number) {
    if (cmt.user.id == this.userInfo.id) {
      this.activeIndex = index;
    }
  }

  onMouseLeave(cmt, index: number) {
    this.activeIndex = -1;
  }

  formatDate(date: Date): string {
    return this.datePipe.transform(date, "hh:mm dd/MM/yyyy");
  }

  // Sửa cmt
  clickEdit(cmt, index) {
    this.activeEdit = index;
    if (this.editor) {
      this.editor.clipboard.dangerouslyPasteHTML(cmt.description);
    }
  }

  actionEdit() {
    if (this.activeEdit != -1) {
      let comment = this.comments[this.activeEdit];
      let editComment = {
        description: this.newCommentText,
        id: comment.id,
        userId: comment.userId,
        ticketId: comment.ticketId,
      };
      this.commentSV.update(editComment).subscribe((res) => {
        this.getData();
        this.actionCancelEdit();
        this.notify.success("Edit comment successfully!");
      });
    }
  }

  actionCancelEdit() {
    this.activeEdit = -1;
    if (this.editor) {
      this.editor.clipboard.dangerouslyPasteHTML("");
      this.newCommentText = "";
    }
  }

  deleteAction(cmt, i) {
    abp.message.confirm(
      "Comment Delete Warning Message",
      undefined,
      (result: boolean) => {
        if (result) {
          this.commentSV.delete(cmt.id).subscribe((res) => {
            if (res.result) {
              this.notify.success("Delete comment successfully!");
              this.getData();
            }
          });
        }
      }
    );
  }
}

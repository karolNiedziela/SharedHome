import { faXmark } from '@fortawesome/free-solid-svg-icons';
import {
  Component,
  ElementRef,
  Output,
  ViewChild,
  EventEmitter,
} from '@angular/core';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.scss'],
})
export class UploadImageComponent {
  @Output() profileImage: EventEmitter<File> = new EventEmitter<File>();

  @ViewChild('fileDropRef', { static: false }) fileDropEl!: ElementRef;

  private allowedFormats: Array<string> = [
    'image/png',
    'image/jpg',
    'image/jpeg',
    'image/gif',
    'image/tiff',
    'image/bpg',
  ];

  file?: File;
  progress: number = 0;
  preview = '';
  error?: string;

  deleteIcon = faXmark;

  constructor() {}

  selectFile(event: any): void {
    this.uploadFile(event.target.files);
  }

  onFileDropped(event: any): void {
    this.uploadFile(event);
  }

  uploadFile(files: Array<any>): void {
    this.preview = '';
    this.progress = 0;
    this.error = null!;

    const file: File | null = files[0];

    if (this.allowedFormats.indexOf(file!.type) === -1) {
      this.error = 'Invalid format';
      return;
    }

    if (file) {
      this.preview = '';
      this.file = file;

      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.preview = e.target.result;
      };

      reader.readAsDataURL(this.file);

      // const formData = new FormData();
      // formData.append('file', file, file.name);

      this.profileImage.emit(file);
    }
  }

  deleteFile(): void {
    this.file = null!;
    this.preview = '';
    this.error = null!;
  }

  formatBytes(bytes: any, decimals = 2) {
    if (bytes === 0) {
      return '0 Bytes';
    }
    const bufferSize = 1024;
    const dm = decimals <= 0 ? 0 : decimals;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    const exponent = Math.floor(Math.log(bytes) / Math.log(bufferSize));
    return (
      parseFloat((bytes / Math.pow(bufferSize, exponent)).toFixed(dm)) +
      ' ' +
      sizes[exponent]
    );
  }
}

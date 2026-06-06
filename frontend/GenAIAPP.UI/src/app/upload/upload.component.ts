import { Component } from '@angular/core';
import { HttpClient ,HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-upload',
  imports: [CommonModule, FormsModule,ReactiveFormsModule,HttpClientModule],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.css',
  
})

export class UploadComponent {
  form: FormGroup;
  isDragging = false
  selectedFile: File | null = null
  apiKey = ""
  outputType = "userStories"
  additionalInstructions = ""
  generatedContent = ""
  loading = false;
  allowedExtensions = [
  '.txt'
  // '.mp3',
  // '.wav',
  // '.mp4',
  // '.avi'
];

constructor(
  private fb: FormBuilder,
  private http: HttpClient,
  private apiService: ApiService
){
  this.form = this.fb.group({
      file: [null],
      outputType: ['userStories']
  });
}

//  constructor(private fb: FormBuilder, private http: HttpClient) {
//     this.form = this.fb.group({
//       file: [null],
//       outputType: ['test-case']
//     });
//   }

  validateFile(file: File): boolean {

  const extension =
    '.' + file.name.split('.').pop()?.toLowerCase();

  if (!this.allowedExtensions.includes(extension)) {

    alert('Unsupported file type');

    return false;
  }

  return true;
}

  onDragOver(event: DragEvent) {
    event.preventDefault()
    event.stopPropagation()
    this.isDragging = true
  }
 onDragLeave() {
    this.isDragging = false
  }

onDrop(event: DragEvent) {
    event.preventDefault()
    event.stopPropagation()
    this.isDragging = false
if (event.dataTransfer?.files.length) {
      this.selectedFile = event.dataTransfer.files[0]
      console.log("File selected:", this.selectedFile.name)
    }
  }

  onFileSelected(event: Event) {
  const fileInput = event.target as HTMLInputElement;

  if (fileInput.files && fileInput.files.length > 0) {

  const file = fileInput.files[0];

  if (!this.validateFile(file))
    return;

  this.selectedFile = file;

  this.form.patchValue({
    file: file
  });
}
}

   removeFile(): void {
    this.selectedFile = null;    
  }

   generateContent() {
    this.loading = true;

  console.log(this.form.value);

  if (!this.form.value.file) {
    alert('Please select a file');
    return;
  }

  const formData = new FormData();

  formData.append('file', this.form.value.file);
  formData.append('outputType', this.form.value.outputType);

  this.apiService.uploadFile(formData).subscribe({
    next: (res) => {
      this.generatedContent = res.content;
      this.loading = false;
    },
    error: (err) => {
      console.error(err);
      alert(JSON.stringify(err));
      this.loading = false;
    }
});

}

}
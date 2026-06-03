import { Component } from '@angular/core';
import { HttpClient ,HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-upload',
  imports: [CommonModule, FormsModule,ReactiveFormsModule,HttpClientModule],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.css',
  providers: [HttpClient]
})

export class UploadComponent {
  form: FormGroup;
  isDragging = false
  selectedFile: File | null = null
  apiKey = ""
  outputType = "userStories"
  additionalInstructions = ""
  generatedContent = ""

 constructor(private fb: FormBuilder, private http: HttpClient) {
    this.form = this.fb.group({
      file: [null],
      outputType: ['test-case']
    });
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
      this.form.patchValue({ file: fileInput.files[0] });
    }
  }

   removeFile(): void {
    this.selectedFile = null;    
  }

   generateContent() {     
    if (!this.form || !this.form.value.file) return;

    const formData = new FormData();
    formData.append('file', this.form.value.file);
    formData.append('outputType', this.form.value.outputType);

    // this.loading = true;

    this.http.post<{ content: string }>('https://localhost:7234/Upload', formData).subscribe({
      next: (res) => {
        this.generatedContent = res.content;
        // this.loading = false;
      },
      error: () => {
        alert('Error generating content');
        // this.loading = false;
      }
    }); 
    // Simulate loading
    this.generatedContent = "Processing..." 
  }
}
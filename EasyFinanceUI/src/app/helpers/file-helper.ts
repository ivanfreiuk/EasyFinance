import { Injectable } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { stringify } from 'querystring';


@Injectable({
    providedIn: 'root'
})
export class FileHelper {

    constructor(private sanitizer: DomSanitizer) { }

    public setImageURL(file: File | Blob, imageURL: string) {
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = (_event) => {
            imageURL = reader.result as string;
            console.log(imageURL)
        };
    }

    public getImageSafeURL(fileBytes: ArrayBuffer, fileName: string): SafeUrl {
        const extension = this.getFileExtension(fileName);
        
        let imageURL = this.sanitizer.bypassSecurityTrustUrl(`data:imgage/${extension};base64,${fileBytes}`);

        return imageURL;
    }

    public getFileExtension(fileName: string): string {
        return fileName.substr(fileName.lastIndexOf('.') + 1);
    }

}
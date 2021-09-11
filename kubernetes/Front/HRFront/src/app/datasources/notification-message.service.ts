import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationMessageService {

  constructor(public snackBar: MatSnackBar) { }

  notifyError(error){
    console.log(error);  
      let errorMessage = 'Se ha producido un error no esperado';
      if(error.error_description && error.error_description > 0){
        errorMessage = error.error_description;
      } 
      else if(error.error && error.error.exceptionMessage){
        errorMessage = error.error.exceptionMessage;
      } 
      else if(error.error && error.error.error_description){
        errorMessage = error.error.error_description;
      }
      else if(error.error_description && error.error_description.length > 0){
        errorMessage = error.error_description;
      }
      else if(error.error && error.error.error){
        errorMessage = error.error.error;
      }
      else if(error.error && error.error.length > 0){
        errorMessage = error.error;
      }

      this.showError(errorMessage);      
  }

  showError(message : string, title : string = 'Error') : void{
    this.snackBar.open(message, 'Cerrar', {
      duration: 10000,
      panelClass: ['red-snackbar'],
      verticalPosition: 'top'
    });
  }

  showInformation(message : string, title : string = 'Información') : void{
    this.snackBar.open(message, 'Cerrar', {
      duration: 8000,
      panelClass: ['green-snackbar'],
      verticalPosition: 'top'
    });
  }

}

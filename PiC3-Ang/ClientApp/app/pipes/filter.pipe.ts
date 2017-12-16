import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
    name:'filter'
})
export class FilterPipe implements PipeTransform {
    transform(value: any, filterString: string, propName: string): any {
        const resultArray = [];
        if (typeof value !== 'undefined') {
            if (value.length === 0 || filterString === '' || filterString.length < 3) {
                return value;
            }
            for (const item of value) {         
                //if (item[propName] === filterString) { }
                for (var key in item) {
                    var value = item[key]; 
                    if (value.toString().toUpperCase().indexOf(filterString.toUpperCase()) >= 0) {
                        resultArray.push(item);
                    }
                }
            }
        }
        return resultArray;
    }
}
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {
    public results: SearchResult[] | null;
    private query = "";
    private queryChanged: Subject<string> = new Subject<string>();
    private searching: boolean = false;
    private http: Http;
    private baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.queryChanged
            .debounceTime(1000)
            .distinctUntilChanged()
            .subscribe(model => {
                this.query = model;
                this.search(this.query);
            });
    }

    onFieldChange(value: string) {
        this.queryChanged.next(value);
    }

    search(term: string) {
        this.results = null;
        this.searching = true;
        this.http.get(`${this.baseUrl}api/thesaurus/${term}`).subscribe(result => {
            this.results = result.json() as SearchResult[];
            this.searching = false;
        }, error => {
            console.error(error);
            this.searching = false;
        });
    }
}

interface SearchResult {
    category: string;
    synonyms: string;
}

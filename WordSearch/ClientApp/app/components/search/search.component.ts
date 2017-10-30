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
    public result: SearchResult | null;
    private query = "";
    private queryChanged: Subject<string> = new Subject<string>();
    private searching: boolean = false;
    private firstLoad: boolean = true;
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
        this.result = null;
        this.searching = true;
        this.firstLoad = false;
        this.http.get(`${this.baseUrl}api/search/${term}`).subscribe(result => {
            this.result = result.json() as SearchResult;
            this.searching = false;
        }, error => {
            console.error(error);
            this.searching = false;
        });
    }
}

interface ThesaurusEntry {
    category: string;
    synonyms: string;
    synonymList: Array<string>;
}

interface Definition {
    serialNumber: string;
    definingText: string;
}

interface DictionaryEntry {
    headword: string;
    wav: string;
    functionalLabel: string;
    date: string;
    wavUrl: string;
    definitions: Array<Definition>;
}

interface SearchResult {
    dictionaryEntries: Array<DictionaryEntry>;
    thesaurusEntries: Array<ThesaurusEntry>;
}

import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '10s',
};

const BASE_URL = 'http://localhost:8080/unit/list';
const TOKEN = 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEsImV4cCI6MTcwMDA3NjA2OSwibmJmIjoxNzAwMDcyNDY5fQ.Hu5Pyk9FSZsRzgr0YNG8dd9kx4XcM16HsnHDKumXXp4';

export default function () {
    const params = {
        page: '-1',
        per_page: '10'
    };

    const headers = {
        headers: {
            'Authorization': `Bearer ${TOKEN}`
        }
    };

    let response = http.get(`${BASE_URL}?page=${params.page}&per_page=${params.per_page}`, headers);

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}

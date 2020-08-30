import appSettings from '../appsettings';
import toDoModel from '../types';

function getHeader(method: string) {

}
export default function clientApi() {
    let strData = sessionStorage.getItem('userData');
    let data = JSON.parse(strData || '') || null;
    let token = data['tokenId'];

    return {
        getAllTodos(tableId: string) {

            return fetch(`${appSettings.serverUrl}listAll?projectId=${tableId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            })
                .then(res => res.json());
        },
        createTodo(dto: toDoModel) {
            return fetch(appSettings.serverUrl, {
                method: 'PUT',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify(dto)
            })
        },
        getTodoInfo(tableId: string) {
            return fetch(`${appSettings.serverUrl}listAll?projectId=${tableId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            })
                .then(res => res.json())
        },
        deleteTodo(id: number) {
            return fetch(`${appSettings.serverUrl}${id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            })
        },
    }
}
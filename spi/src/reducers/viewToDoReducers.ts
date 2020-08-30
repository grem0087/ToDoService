import {
    CREATE_TODO_REQUEST,
    CREATE_TODO_SUCCESS,
    UPDATE_TODO_REQUEST,
    DELETE_TODO_REQUEST,
    SWITCH_UPDATE_WINDOW,
    SWITCH_SIGNIN_WINDOW,
    GET_ALL_TODOS_REQUEST,
    GET_ALL_TODOS_SUCCESS,
    TODO_REQUEST_FAILED
} from '../actions/todo-action-types';

const toDoItems: any[] = [];
let initialState = {
    isSignInOpen: false,
    open: false,
    toDoItems
};

export const toDoCrudReducer = (state = initialState, action) => {
    const { type, payload } = action;
    //reducer debug
    console.warn(`reducer catch: ${type} with`);
    console.warn(payload);
    switch (type) {
        case TODO_REQUEST_FAILED:
            return {
                ...state,
                isLoading: false,
                error: true,
            }

        case GET_ALL_TODOS_SUCCESS:
            state.toDoItems = payload.items;
            return {
                ...state,
                isLoading: false,
                error: false,
            }

        case GET_ALL_TODOS_REQUEST:
            return {
                ...state,
                isLoading: true,
                error: false,
            }

        case CREATE_TODO_REQUEST:
            return {
                ...state,
                dto: payload.dto,
                isLoading: true,
                error: false,
            };

        case CREATE_TODO_SUCCESS:
            state.toDoItems.push(payload);
            return {
                ...state,
                isLoading: false,
                error: false,
            }

        case SWITCH_SIGNIN_WINDOW:
            return {
                ...state,
                isSignInOpen: payload.isSignInOpen,
            };

        case SWITCH_UPDATE_WINDOW:
            return {
                ...state,
                open: payload.open,
            };

        case UPDATE_TODO_REQUEST:
            return {
                ...state,
                order: payload
            };

        case DELETE_TODO_REQUEST:
            state.toDoItems.pop();
            return state;

        default: return state;
    }
};

export default toDoCrudReducer;

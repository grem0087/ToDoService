import { all, fork } from 'redux-saga/effects';
import watchCrudUpdate from './todoCrudSagas';

const sagas = [
  watchCrudUpdate
];

export const rootSaga = function* root() {
  yield all(sagas.map(saga =>fork(saga)));
};
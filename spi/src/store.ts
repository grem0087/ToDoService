import { createStore, compose, applyMiddleware } from 'redux';
import createSagaMiddleware from 'redux-saga';
import { rootReducers } from './reducers';
import { rootSaga } from './sagas';

export default function configureStore() {
  const sagaMiddleware = createSagaMiddleware();

  const composeEnhancer =
    (process.env.NODE_ENV !== 'production' &&
      window['__REDUX_DEVTOOLS_EXTENSION_COMPOSE__']) ||
    compose;

  const store = createStore(
    rootReducers,
    {},
    composeEnhancer(applyMiddleware(sagaMiddleware))
  );

  sagaMiddleware.run(rootSaga);
  return store;
}
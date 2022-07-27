import store from "../../state/store"
import LoadingStateService from "../../services/LoadingStateService"
import APICallProps from "./interfaces/APICallProps"

import { toast } from 'react-toastify';
import ErrorToast from "src/components/Toast/DefaultErrorToast";

const APICallWrapper = async (
    {
        url,
        options,
        onSuccess = async (response) => { },
        onFailure = async (response) => { },
        onFinal = async () => { },
        showSuccess = false,
        successMesage = 'Success',
        showError = true,
    }: APICallProps
) => {
    try {
        LoadingStateService.StartLoading()

        if (!options.headers) {
            options.headers = {};
        }

        let auth = store.getState().auth;

        if (auth.isAuthenticated) {
            options.headers["Authorization"] = `Bearer ${auth.token}`;
        }

        options.headers["Content-Type"] = "application/json";

        const response = await fetch(url, options);

        if (response.status == 200) {
            await onSuccess(response)

            if (showSuccess) {
                toast.success(successMesage, {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
            return
        }

        let error = await response.json();

        await onFailure(response)

        if (showError) {
            ErrorToast(error.detail);
        }

    } catch (error) {
        ErrorToast(error);
    } finally {
        await onFinal();
        LoadingStateService.FinishLoading()
    }
};


export default APICallWrapper;

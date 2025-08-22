<?php

namespace App\Http\Controllers\Todo;

use App\Http\Controllers\Controller;
use App\Services\CategoryService;
use Exception;
use Illuminate\Http\Request;

class CategoryController extends Controller
{
    private $service;
    private $storeRules = [
        'name'          => 'required|max:255',
        'description'   => 'required',
    ];
    function __construct()
    {
        $this->service = new CategoryService();
    }
    public function store(Request $request)
    {

        $validated = $request->validate($this->storeRules);
        if (!$validated) {
            return response()
                ->json([
                    "status" => "error",
                    "error" => $validated
                ])
                ->setStatusCode(400)
                ;
        }
        try {
            $data = $request->all();
            $id = $data["id"];
            $name = $data["name"];
            $description = $data["description"];
            $data = $this->service->store($id, $name, $description);
            return response()
                ->json([
                    "status" => "success",
                    "data" => $validated
                ])
                ->setStatusCode(200);
        } catch (Exception $e) {
            return response()
                ->json([
                    "status" => "error",
                    "error" => $e->getMessage()
                ])
                ->setStatusCode(400);
        }
    }

    public function getCategoryById(int $id) {
        try{
            return response()
                ->json([
                    "status" => "success",
                    "data" => $this->service->getById($id)
                ])
                ->setStatusCode(200);
        } catch(Exception $e) {
            return response()
                ->json([
                    "status" => "error",
                    "error" => $e->getMessage()
                ])
                ->setStatusCode(400);
        }
    }
    public function getCategories()
    {
        return $this->service->getCategories();
    }
}

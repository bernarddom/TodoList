<?php

namespace App\Http\Controllers\Todo;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use TodoListService;

class CategoryController extends Controller
{
    private $service;
    function __construct(){
        $this->service = new Catre();
    }
    public function store(Request $request) {
        $data = $request->all();
        return $data;
        $this->service->store();
    }
    public function show() {
        // return $this->;
    }
}
